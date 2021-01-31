// https://atcoder.jp/contests/abs/tasks/arc065_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
    |> ((s) =>
      List("eraser", "erase", "dreamer", "dream")
        .foldLeft(List(s))((acc, word) =>
          acc.flatMap((x) =>
            x.split(word).filter(_ != "")
          )
        )
        .length
    )
    |> ((x) => if (x == 0) "YES" else "NO")
    |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19834985
