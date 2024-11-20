// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=2502

object Main {
  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def solve(slp: Array[(Int, Int, Int)], w: Array[Int]) = {
    val MAX_LEN = 393
    val dp = Array.fill(MAX_LEN + 1)(-1L)
    dp(0) = 0
    slp.foreach { case (s, l, p) =>
      (0 to MAX_LEN)
        .foreach(i =>
          if (dp(i) != -1)
            (s to l)
              .filter(i + _ <= MAX_LEN)
              .foreach(j =>
                dp(i + j) = Math.max(dp(i) + p, dp(i + j))
              )
        )
    }
    val ans = w.map(dp(_))
    if (ans.contains(-1)) Array(-1) else ans
  }

  def main(args: Array[String]) = {
    val n = io.StdIn.readLine.toInt
    val slp = (1 to n).map(_x => io.StdIn.readLine.split(" ").map(_.toInt) |> (x => (x(0), x(1), x(2)))).toArray
    val m = io.StdIn.readLine.toInt
    val w = (1 to m).map(_x => io.StdIn.readLine.toInt).toArray
    println(solve(slp, w).mkString("\n"))
  }
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=9870545#1
