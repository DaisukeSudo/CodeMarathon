// https://atcoder.jp/contests/abs/tasks/abc081_b

fun main(args: Array<String>) = (
  readLine()!!
  pp { _x: Any ->
    readLine()!!
      .split(" ")
      .map(String::toInt)
      .reduce { x, a -> x or a }
      .toString(2)
  }
  pp { x -> x.length - x.lastIndexOf('1') - 1 }
  pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/20984743
