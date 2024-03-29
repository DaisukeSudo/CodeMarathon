// ----- 基本 -----

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine()
    |> println
  )
}


// ----- デバッグ -----

implicit class ArrayString[T](x: Array[T]) {
  def toArrayString: String = x.mkString("[", ",", "]")
}


// ----- シーケンス -----

// 無限シーケンス
Stream.continually(io.StdIn.readLine())
  .takeWhile(_ != "0")

Iterator.continually(io.StdIn.readLine())
  .takeWhile(_ != "0")
  .toArray


// ----- 配列 -----

// 配列の生成（同値）
Array.fill[Int](n)(0)

// 配列の生成（連続値）
Array.range(0, n)

// ２次元配列の生成
(n => (Array.range(0, n).map(_x => Array.fill[Int](n)(0))))

// ２次元配列の表示
(_.map(_.mkString("[", ", ", "]")).mkString("\n"))


// ----- 再帰 -----

(
  Array((_x: Int, _y: Int) => 0)
)
|> (fnc => (
    (x: Int, y: Int) =>
      if (y == 1) x else fnc(0)(x * 2, y - 1)
  )
  |> (fn => (
    (fnc(0) = fn)
    |> (_ => fn(1, 10))
  ))
)


// ----- マッチング -----

(ao: Option[Int]) =>
  ao match {
    case Some(a) => "%d".format(a)
    case _ => "None"
  }

(x: Int) =>
  x match {
    case x if x < 0  => "Negative"
    case x if x == 0 => "Zero"
    case _ => "Positive"
  }


// ----- 関数 -----

// 2点間の距離（2次元座標）
val distance2 = (v1: Array[Double], v2: Array[Double]) =>
  List.range(0, 2).map(i => v1(i) - v2(i)).map(x => x * x).sum.let(sqrt)

// 2点間の距離（3次元座標）
val distance3 = (v1: Array[Double], v2: Array[Double]) =>
  List.range(0, 3).map(i => v1(i) - v2(i)).map(x => x * x).sum.let(sqrt)

// べき乗
val power = (p: Long) => (
  (Array((_m: Long, _n: Long) => 0L))
  |> (fnc => (
    (m: Long, n: Long) =>
      n match {
        case _x if n == 0L => 1L
        case _x if n % 2L == 0L => fnc(0)(m, n / 2L) |> (x => x * x % p)
        case _ => m * fnc(0)(m, n - 1L) % p
      }
    )
    |> (fn => (fnc(0) = fn) |> (_x => fn))
  )
)
