// https://atcoder.jp/contests/s8pc-1/tasks/s8pc_1_e

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  val p = 1_000_000_007L

  val power = (
    (Array((_m: Long, _n: Long) => 0L))
    |> (fnc => (
      (m: Long, n: Long) =>
        n match {
          case _x if n == 0L => 1L
          case _x if n % 2L == 0L => fnc(0)(m, n / 2L) |> (x => x * x % p)
          case _ => m * fnc(0)(m, n - 1L) % p
        }
      )
      |> (fn => (fnc(0) = fn) |> (_x => fn))
    )
  )

  val accCost = (arr: Array[Long]) => (
    arr
      .tail
      .foldLeft((List(0L), arr.head)) { case ((acc, i1), i2) =>
        ((acc.head + power(i1, i2)) :: acc, i2)
      }
      ._1
      .reverse
      |> (Array(0L) ++ _)
  )

  val totalCost = (acc: Array[Long], route: Array[Int]) => (
    (route ++ Array(1))
      .foldLeft((0L, 1)) { case ((cost, i1), i2) =>
        (cost + (acc(i1) - acc(i2)).abs) |> (x => (x % p, i2))
      }
      ._1
  )

  def main(args: Array[String]) = (
    (
      (io.StdIn.readLine),
      (io.StdIn.readLine.split(' ').map(_.toLong) |> accCost |> totalCost.curried),
      (io.StdIn.readLine.split(' ').map(_.toInt)),
    )
    |> { case (_x, solve, route) => route |> solve }
    |> println
  )
}

// https://atcoder.jp/contests/s8pc-1/submissions/25010831
