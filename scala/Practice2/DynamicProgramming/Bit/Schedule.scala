// https://atcoder.jp/contests/joi2014yo/tasks/joi2014yo_d

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      io.StdIn.readLine.toInt,
      ("_" + io.StdIn.readLine)
        .map(c =>
          c match {
            case 'J' => 1
            case 'O' => 2
            case 'I' => 4
            case _   => 0
          }
        )
        .toArray
    )
    |> { case (n, rs) => (
      Array.range(0, n + 1).map(_x => Array.fill[Int](8)(0))
      |> (memo => (
        (memo(0)(1) = 1)
        |> (_x =>
          for (
            i <- Stream.range(1, n + 1);
            j <- Stream.range(1, 8)
              if (j & rs(i)) == rs(i);
            j2 <- Stream.range(1, 8)
              if (0 to 2).exists(x => ((j >> x & 1) == 1) && ((j2 >> x & 1) == 1))
          ) (
            memo(i)(j) = memo(i)(j) + memo(i - 1)(j2) % 10007
          )
        )
        |> (_x => memo(n).sum % 10007)
      ))
    )}
    |> println
  )
}

// https://atcoder.jp/contests/joi2014yo/submissions/20781702
