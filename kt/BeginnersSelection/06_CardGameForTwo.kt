// https://atcoder.jp/contests/abs/tasks/abc088_b

fun main(args: Array<String>) = (
  readLine()!!
  pp { _x: Any ->
    readLine()!!
      .split(" ")
      .map(String::toInt)
      .sorted()
      .reversed()
      .withIndex()
      .fold(0 to 0, { a, x ->
        if (x.index % 2 == 0)
          a.first + x.value to a.second
        else
          a.first to a.second + x.value
      })
  }
  pp { (a, b) -> a - b }
  pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/21003740
