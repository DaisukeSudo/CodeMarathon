// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_g

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (() => io.StdIn.readLine.split(' '))
    |> (readLine => (
      readLine().map(_.toInt)
      |> (arr => (arr(0), arr(1)))
      |> { case (n, m) => (
        (
          List.range(0, m)
            .map(_x => (readLine() |> (arr => (arr(0).toInt - 1, arr(1).toInt - 1, arr(2).toLong, arr(3).toLong))))
            .map { case (s, t, d, time) => if (s < t) (s, t, d, time) else (t, s, d, time) },
          Array.range(0, 1 << n)
            .map(_x => Array.fill[Tuple2[Long, Long]](n)(Long.MaxValue, 0L))
        )
        |> { case (graph, memo) => (
          (memo(1)(0) = (0L, 1L))
          |> (_x =>
            Stream.empty
              .append(
                for (
                  vx <- 1 until (1 << n) by 2;
                  (s, t, d, time) <- graph if (vx >> s & 1) == 1 && (vx >> t & 1) == 1;
                  x <- Stream(
                    (vx, t, d, time, vx ^ (1 << t), s),
                    (vx, s, d, time, vx ^ (1 << s), t)
                  )
                )
                yield x
              )
              .append(
                for (
                  (s, t, d, time) <- graph if s == 0;
                  x <- Stream(
                    (0, 0, d, time, (1 << n) - 1, t)
                  )
                )
                yield x
              )
              .foreach { case (i, j, d, time, i2, j2) => (
                memo(i2)(j2)
                |> { case (prevCost, prevCount) =>
                  if (prevCost != Long.MaxValue && prevCost + d <= time) (
                    (memo(i)(j), prevCost + d)
                    |> { case ((accCost, accCount), curCost) =>
                      i match {
                        case _x if curCost < accCost => (curCost, prevCount)
                        case _x if curCost == accCost => (accCost, accCount + prevCount)
                        case _x => (accCost, accCount)
                      }
                    }
                    |> (v => memo(i)(j) = v)
                  )
                }
              )}
          )
          |> (_x => memo(0)(0))
        )}
      )}
    ))
    |> { case (cost, count) => if (count == 0L) "IMPOSSIBLE" else s"${cost} ${count}" }
    |> println
  )
}
