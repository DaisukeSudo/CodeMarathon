// https://atcoder.jp/contests/joi2008ho/tasks/joi2008ho_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Array(List.empty[Tuple2[String, Int]]) |> (line => (
      io.StdIn.readLine.toInt |> (n => (1 to n).foreach(i =>
        io.StdIn.readLine |> (x =>
          if (i % 2 == 1)
            line(0) match {
              case (y, size)::ts if x == y => line(0) = (x, size + 1)::ts
              case _ => line(0) = (x, 1)::line(0)
            }
          else
            line(0) match {
              case (y, size)::ts if x == y => line(0) = (x, size + 1)::ts
              case (_, size1)::ts1 =>
                ts1 match {
                  case (_, size2)::ts2 => line(0) = (x, size1 + size2 + 1)::ts2
                  case _ => line(0) = List((x, size1 + 1))
                }
              case _ => ()
            }
        )
      ))
      |> (_ => line(0)
        .filter { case (x, _) => x == "0" }
        .map { case (_, size) => size }
        .sum
      )
    ))
    |> println
  )
}

// https://atcoder.jp/contests/joi2008ho/submissions/28714149
