// https://atcoder.jp/contests/abs/tasks/abc085_c

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    .let { a -> a[0] to a[1] / 1000 }
    .let { (n, y) -> (
      (0..n)
        .find { a  ->
          (y - n - 9 * a)
            .let { x -> x % 4 == 0 && x >= 0 && n - a - x / 4 >= 0 }
        }
        ?.let { a ->
          ((y - n - 9 * a) / 4)
            .let { b -> "${a} ${b} ${n - a - b}" }
        }
        ?: "-1 -1 -1"
    )}
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21477216
