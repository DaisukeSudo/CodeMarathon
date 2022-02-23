// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=1149

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    Stream.continually(io.StdIn.readLine())
      .takeWhile(_ != "0 0 0")
      .map(_.split(" ").map(_.toInt))
      .foldLeft(0, 0, List.empty[Tuple2[Int, Int]], List.empty[Array[Tuple2[Int, Int]]]) {
        case ((n, i, ss, pss), a) =>
          if (n == i) (
            (a(0), a(1), a(2)) |> { case (n, w, d) =>
              (n, 0, (w, d) :: ss, Array.fill(n)((0, 0)) :: pss)
            }
          )
          else (
            (pss(0)(i) = (a(0), a(1))) |> (_x =>
              (n, i + 1, ss, pss)
            )
          )
      }
      |> { case (_n, _i, ss, pss) => ss zip pss }
      |> (_.reverse)
      |> (_.map { case ((w, d), ps) => (
        ps.foldLeft(Array((w, d))) { case (acc: Array[Tuple2[Int, Int]], (p, s)) =>
          acc.slice(0, p - 1) ++
          acc.slice(p, acc.length) ++ (
            acc(p - 1) |> { case (w, d) => (
              (s % (w + d)) |> (x =>
                if (x < w)
                  Array((x, d), (w - x, d))
                else
                  Array((w, x - w), (w, d - x + w))
              )
            )}
            |> (_.sortBy { case (w, d) => w * d })
          )
        }
        |> (_.map { case (w, d) => w * d })
        |> (_.sorted)
        |> (_ mkString " ")
      )})
      |> (_ foreach println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=6349947
// https://onlinejudge.u-aizu.ac.jp/status/users/dsudo/submissions/1/1149/judge/6349947/Scala
