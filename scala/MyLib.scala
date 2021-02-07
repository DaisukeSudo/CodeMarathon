// ----- 再帰 -----

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    (
      Array((_x: Int, _y: Int) => 0)
    )
    |> (fnc => (
        (x: Int, y: Int) =>
          if (y == 1) x else fnc(0)(x, y - 1)
      )
      |> (fn => (
        (fnc(0) = fn)
        |> (_ => fn(123, 10))
      ))
    )
    |> println
  )
}
