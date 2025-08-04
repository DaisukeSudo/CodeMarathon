// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=2415

object Main {

  def solve(n: Int, ws: Array[Long]) = {
    // dp(i)(j) : 区間 [i..j] を全て1センチメートル区切りにするための最小コスト
    val dp   = Array.tabulate(n, n)((i, j) => if (i == j) 0L else Long.MaxValue)
    // opt(i)(j): 区間 [i..j] を分割する最適な位置kを記録
    val opt  = Array.tabulate(n, n)((i, j) => if (i == j) i else 0)
    // 区間の重さを素早く計算するための累積和
    val psum = ws.scanLeft(0L)(_ + _)

    // len: 更新する区間 (2 to n)
    // i  : 区間の左端のインデックス
    // j  : 区間の右端のインデックス
    // k  : 区間 [i..j] をどこで分割するかを表す位置
    //      k で分割すると [i..k] と [k+1..j] の2つの区間になる
    //      k の範囲は opt(i)(j - 1) から opt(i + 1)(j) の間である (if k < j)
    //
    // Knuth の最適化による k の探索範囲の限定
    // - opt(i)(j) は区間 [i..j] の最適な分割位置
    // - 区間 [i..j] の最適な分割位置は，より小さい区間の最適な分割位置の間に存在する
    // e.g.
    // 1. 区間 [0..3] を処理する場合 k の範囲は以下の2つの値の間
    //    - opt(0)(2): 区間 [0..2] の最適な分割位置
    //    - opt(1)(3): 区間 [1..3] の最適な分割位置
    // 2. もし opt(0)(2) = 1, opt(1)(3) = 2 なら
    //    - k は 1 から 2 の範囲のみを探索
    //    - これにより探索範囲が削減される
    for (len <- 2 to n; i <- 0 to n - len) {
      val j = i + len - 1
      val v = psum(j + 1) - psum(i)
      for (k <- opt(i)(j - 1) to opt(i + 1)(j) if k < j) {
        // 区間 [i..j] のコスト計算
        // v            : 今回切断するのにかかるコスト
        // dp(i)(k)     : （問題上はこの後）左側の区間を分割するコスト（計算上は導出済）
        // dp(k + 1)(j) : （問題上はこの後）右側の区間を分割するコスト（計算上は導出済）
        val cost = v + dp(i)(k) + dp(k + 1)(j)
        // より小さいコストが見つかれば更新
        if (cost < dp(i)(j)) {
          dp(i)(j) = cost
          opt(i)(j) = k
        }
      }
    }
    
    dp(0)(n - 1)
  }

  def main(args: Array[String]) = {
    val n = io.StdIn.readLine.toInt
    val ws = io.StdIn.readLine.split(" ").map(_.toLong)
    println(solve(n, ws))
  }
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=10783554
