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


// ----- シーケンス -----

// 無限シーケンス
Stream.continually(io.StdIn.readLine())
  .takeWhile(_ != "0")


// ----- 配列 -----

// 配列の生成（同値）
Array.fill[Int](n)(0)

// 配列の生成（連続値）
Array.range(0, n)

// 配列の表示
(_.mkString("[", ", ", "]"))

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
