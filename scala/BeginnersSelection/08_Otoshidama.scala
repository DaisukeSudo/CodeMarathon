// https://atcoder.jp/contests/abs/tasks/abc085_c

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      |> (a => (a(0), a(1) / 1000))
      |> { case (n, y) => (
        (0 to n)
          .find(a => (
            (y - n - 9 * a)
            |> (x => x % 4 == 0 && x >= 0 && n - a - x / 4 >= 0)
          ))
          |> ((ao: Option[Int]) =>
            ao match {
              case Some(a) => (
                ((y - n - 9 * a) / 4)
                |> (b => "%d %d %d".format(a, b, (n - a - b)))
              )
              case None => "-1 -1 -1"
            }
          )
      )}
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/21476736
