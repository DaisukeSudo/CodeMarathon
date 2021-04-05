// https://atcoder.jp/contests/abs/tasks/arc065_a

fun main(args: Array<String>) = (
  readLine()!!
    .let { s ->
      listOf("eraser", "erase", "dreamer", "dream")
        .fold(listOf(s)) { acc, word ->
          acc.flatMap { x ->
            x.split(word).filter { it != "" }
          }
        }
    }
    .size
    .let { if (it == 0) "YES" else "NO" }
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21009434
