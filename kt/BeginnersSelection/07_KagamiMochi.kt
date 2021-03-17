// https://atcoder.jp/contests/abs/tasks/abc085_b

fun main(args: Array<String>) = (
  readLine()!!.toInt()
  pp { n: Int ->
    (1..n)
      .map { _x -> readLine()!! }
      .distinct()
      .size
  }
  pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/21003858
