// https://atcoder.jp/contests/joi2017yo/tasks/joi2017yo_d

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      .let(arr => (arr(0), arr(1)))
      .let { case (n, m) =>
        (
          Array.range(0, n).map(_x => io.StdIn.readLine.toInt.let(_ - 1)),
          Array.fill(m)(0),
          Array.range(0, m).map(_x => Array.fill(n + 1)(0)),
          Array.fill(1 << m)(0, 0)
        )
        .let { case (items, cs, vss, memo) =>
          items.foldLeft(0)((i, x) =>
            (
              (cs(x) = cs(x) + 1),
              ((0 until m).foreach(j => vss(j)(i + 1) = cs(j)))
            )
            .let(_x => i + 1)
          )
          .let(_x =>
            (1 until 1 << m).foreach(i =>
              (0 until m)
                .filter(j => (i & (1 << j)) == (1 << j))
                .map(j =>
                  (memo(i ^ (1 << j)), cs(j), vss(j))
                    .let { case ((pc, pv), c, vs) =>
                      (pc + c, pv + (c - vs(pc + c) + vs(pc)))
                    }
                )
                .minBy { case (c, v) => v }
                .let(x => memo(i) = x)
            )
          )
          .let(_x => memo((1 << m) - 1)._2)
        }
      }
      .let(println)
  )
}

// https://atcoder.jp/contests/joi2017yo/submissions/21141200
