// https://atcoder.jp/contests/abs/tasks/abc083_b

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    .let { x -> Triple(x[0], x[1], x[2]) }
    .let { (n, a, b) ->
      (1..n)
        .filter { i ->
          i.toString()
            .map(Char::toString)
            .map(String::toInt)
            .sum()
            .let { x -> a <= x && x <= b }
        }
        .sum()
    }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21021884
