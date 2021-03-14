// https://atcoder.jp/contests/joi2014yo/tasks/joi2014yo_d

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      io.StdIn.readLine.toInt,
      io.StdIn.readLine
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
      (0 until n)
        .foldLeft(
          Array.concat(Array(1), Array.fill[Int](6)(0))
        )((memo, i) =>
          Array.range(1, 8).map(j =>
            if ((j & rs(i)) != rs(i))
              0
            else (
              (1 to 7).map(k =>
                if ((0 to 2).exists(x => ((j >> x & 1) == 1) && ((k >> x & 1) == 1)))
                  memo(k - 1) % 10007
                else
                  0
              )
              .sum
            )
          )
        )
        .sum
        |> (v => v % 10007)
    )}
    |> println
  )
}

// https://atcoder.jp/contests/joi2014yo/submissions/20928844
