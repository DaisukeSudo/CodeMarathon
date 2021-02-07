// https://atcoder.jp/contests/joi2015ho/tasks/joi2015ho_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine.toInt
    |> ((n) =>
      (
        ((1 to n).map(_x => io.StdIn.readLine.toLong).toArray |> (arr => Array.concat(arr, arr))),
        ((1 to n).map(_x => Array.fill[Long](n)(0))).toArray,
        Array((_s: Int, _c: Int) => 0L)
      )
      |> { case (cakes, memo, fnc) => (
        ((start: Int, count: Int) => (
          count match {
            case 0 => cakes(start)
            case x if x < 0 => 0L
            case _ if memo(start % n)(count) != 0L => memo(start % n)(count)
            case _ => (
              List(
                (start,         List((start + 1, 2), (start + count, 1))),
                (start + count, List((start, 1), (start + count - 1, 0))),
              )
              .map {
                case (joi, ioi) => (
                  ioi.maxBy { case (i, _x) => cakes(i) }
                  |> { case (_x, offset) => fnc(0)((start + offset), (count - 2)) }
                  |> (_ + cakes(joi))
                )
              }
              .max
              |> (v => (memo(start % n)(count) = v) |> (_ => v))
            )
          }
        ))
        |> (fn => (
          (fnc(0) = fn)
          |> (_ => (
            (0 until n)
              .map(i => fn(i, (n - 1)))
              .max
          ))
        ))
      )}
    )
    |> println
  )
}

// https://atcoder.jp/contests/joi2015ho/submissions/20033778
