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
          Array.range(0, 1 << n).map(_x => Array.fill[Long](n)(Long.MaxValue)),
          Array.range(0, 1 << n).map(_x => Array.fill[Long](n)(0L))
        )
        |> { case (graph, memV, memC) => (
          (memV(1)(0) = 0L, memC(1)(0) = 1L)
          |> (_x =>
            (i: Int, j: Int, i2: Int, j2: Int, d: Long, time: Long) => (
              memV(i2)(j2)
              |> (preV =>
                if (preV != Long.MaxValue) (
                  preV + d
                  |> (curV =>
                    if (curV <= time) (
                      (memV(i)(j), memC(i)(j), memC(i2)(j2))
                      |> { case (accV, accC, preC) =>
                        i match {
                          case _x if curV < accV  => (curV, preC)
                          case _x if curV == accV => (accV, accC + preC)
                          case _x => (accV, accC)
                        }
                      }
                      |> { case (v, c) => (memV(i)(j) = v, memC(i)(j) = c) }
                    )
                  )
                )
              )
            )
          )
          |> (fn =>
            (
              for (
                vx <- 1 until (1 << n) by 2;
                (s, t, d, time) <- graph if (vx >> s & 1) == 1 && (vx >> t & 1) == 1;
                (i, j, i2, j2) <- Stream(
                  (vx, t, vx ^ (1 << t), s),
                  (vx, s, vx ^ (1 << s), t)
                )
              ) fn(i, j, i2, j2, d, time),
              for (
                (s, t, d, time) <- graph if s == 0;
                (i, j, i2, j2) <- Stream(
                  (0, 0, (1 << n) - 1, t)
                )
              ) fn(i, j, i2, j2, d, time)
            )
          )
          |> (_x => (memV(0)(0), memC(0)(0)))
        )}
      )}
    ))
    |> { case (v, c) => if (c == 0L) "IMPOSSIBLE" else s"${v} ${c}" }
    |> println
  )
}

// https://atcoder.jp/contests/s8pc-1/submissions/20734933
