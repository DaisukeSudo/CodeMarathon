// https://atcoder.jp/contests/nikkei2019-final/tasks/nikkei2019_final_a

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      io.StdIn.readLine.toInt,
      io.StdIn.readLine.split(' ').map(_.toLong)
    )
    |> { case (n, ks) => (
      ks.foldLeft(List(0L)) { case (h::ts, x) => (x + h)::h::ts }
        .reverse
        .toArray
        |> (acc =>
          (1 to n).map(k =>
            (0 to n - k)
              .map(i => acc(i + k) - acc(i))
              .max
          )
        )
    )}
    |> (_ foreach println)
  )
}

// https://atcoder.jp/contests/nikkei2019-final/submissions/26541554
