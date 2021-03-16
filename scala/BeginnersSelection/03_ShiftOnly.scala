// https://atcoder.jp/contests/abs/tasks/abc081_b

object Main {

  implicit class Pipeline[T](x: T) {
    def |>[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      |> (_ => io.StdIn.readLine)
      |> (_.split(" ").map(_.toInt).reduce((x, a) => x | a))
      |> (_.toBinaryString)
      |> ((x) => x.length - x.lastIndexOf('1') - 1)
      |> println
  )
}

// https://atcoder.jp/contests/abs/submissions/19754776
