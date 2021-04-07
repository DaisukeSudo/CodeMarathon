// https://atcoder.jp/contests/abc134/tasks/abc134_e

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) =
    io.StdIn.readLine
      .toInt
      .let(n =>
        Array.fill(n + 1)(math.pow(10, 9) + 1).let(memo =>
          List.range(0, n)
            .map(_x => io.StdIn.readLine.toInt)
            .foldLeft(0) { case (len, x) =>
              if (memo(len) >= x)
                (memo(len + 1) = x)
                  .let(_x => len + 1)
              else
                (1 to 17)
                  .foldLeft((1, len)) { case ((li, ri), _i) =>
                    (li + (ri - li) / 2).let(mi =>
                      if (memo(mi) < x) (li, mi) else (mi + 1, ri)
                    )
                  }
                  .let { case (i, _i) => memo(i) = x }
                  .let(_x => len)
            }
        )
      )
      .let(println)
}

// https://atcoder.jp/contests/abc134/submissions/21552453
