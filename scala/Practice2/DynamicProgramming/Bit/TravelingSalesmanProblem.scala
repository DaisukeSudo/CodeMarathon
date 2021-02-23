// http://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_2_A

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (() => io.StdIn.readLine().split(' ').map(_.toInt))
    |> (readLine => (
      readLine()
      |> (arr => (arr(0), arr(1)))
      |> { case (v, e) => (
        (
          (1 to e)
            .map(_x => (readLine() |> (arr => (arr(0), arr(1), arr(2)))))
            .map { case (s, t, d) => (s, (if (t == 0) v else t), d) },
          Array.range(0, 1 << v + 1).map(_x => Array.fill[Int](v + 1)(Int.MaxValue))
        )
        |> { case (graph, memo) => (
          (memo(1)(0) = 0)
          |> { case () =>
            (
              for (
                vx <- 1 to 1 << v + 1;
                (s, t, d) <- graph
                if (vx & 1) == 1 && (vx >> s & 1) == 1 && (vx >> t & 1) == 1
              )
              yield (vx, s, t, d)
            )
            .foreach { case (vx, s, t, d) => (
              memo(vx ^ (1 << t))(s)
              |> (prev =>
                if (prev != Int.MaxValue)
                  memo(vx)(t) = (memo(vx)(t) min (prev + d))
                else
                  ()
              )
            )}
          }
          |> (_x => memo.last.last)
        )}
      )}
    ))
    |> (x => if (x == Int.MaxValue) -1 else x)
    |> println
  )
}

// http://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5245114
