// https://atcoder.jp/contests/abs/tasks/abc081_b

fun main(args: Array<String>) = (
  readLine()!!
    .let { _ ->
      readLine()!!
        .split(" ")
        .map(String::toInt)
        .reduce { x, a -> x or a }
        .toString(2)
    }
    .let { x -> x.length - x.lastIndexOf('1') - 1 }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21021723
