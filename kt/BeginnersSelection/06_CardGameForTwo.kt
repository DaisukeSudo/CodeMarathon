// https://atcoder.jp/contests/abs/tasks/abc088_b

fun main(args: Array<String>) = (
  readLine()!!
    .let { _ ->
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
    .let { (a, b) -> a - b }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21021940
