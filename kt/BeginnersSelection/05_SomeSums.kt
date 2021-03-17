// https://atcoder.jp/contests/abs/tasks/abc083_b

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    pp { x -> Triple(x[0], x[1], x[2]) }
    pp { (n: Int, a: Int, b: Int) ->
      (1..n)
        .filter { i -> (
          i.toString()
            .map(Char::toString)
            .map(String::toInt)
            .sum()
            pp { x -> a <= x && x <= b }
        )}
        .sum()
    }
    pp ::println
)

infix fun <T, R> T.pp(f: (T) -> R): R = f(this)

// https://atcoder.jp/contests/abs/submissions/21002519
