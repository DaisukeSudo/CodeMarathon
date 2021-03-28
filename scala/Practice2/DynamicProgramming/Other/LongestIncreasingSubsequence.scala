// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=DPL_1_D

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .toInt
      .let(n =>
        (
          List.range(0, n).map(_x => io.StdIn.readLine.toInt),
          Array.fill(n + 1)(-1)
        )
        .let { case (ns, memo) =>
          ns.foldLeft(0) { case (len, x) =>
            if (memo(len) < x)
              (memo(len + 1) = x)
                .let(_x => len + 1)
            else
              memo.indexWhere(_ >= x)
                .let(i => memo(i) = x)
                .let(_x => len)
          }
        }
      )
      .let(println)
  )
}

// https://onlinejudge.u-aizu.ac.jp/recent_judges/DPL_2_A/judge/5320964/dsudo/Scala
