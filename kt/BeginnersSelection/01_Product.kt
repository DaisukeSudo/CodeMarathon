// https://atcoder.jp/contests/abs/tasks/abc086_a

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    .map { it and 1 }
    .reduce { a, x -> a and x }
    pp { if (it == 1) "Odd" else "Even" }
    pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/20984243
