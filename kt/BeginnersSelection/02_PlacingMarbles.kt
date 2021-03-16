// https://atcoder.jp/contests/abs/tasks/abc081_a

fun main(args: Array<String>) = (
  readLine()!!
    .map(Char::toString)
    .map(String::toInt)
    .sum()
    pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/20984377
