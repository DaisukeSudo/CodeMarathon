// https://atcoder.jp/contests/abs/tasks/abc085_c

fun main(args: Array<String>) = (
  readLine()!!
    .split(" ")
    .map(String::toInt)
    .let { a -> a[0] to a[1] / 1000 }
    .let { (n, y) -> (
      (0..n)
        .find { a  ->
          (y - n - 9 * a) % 4 == 0 &&
            (y - n - 9 * a) >= 0 &&
            n - a - (y - n - 9 * a) / 4 >= 0
        }
        ?.let { a ->
          ((y - n - 9 * a) / 4)
            .let { b -> "%d %d %d".format(a, b, (n - a - b)) }
        }
        ?: run { "-1 -1 -1" }
    )}
    .let(::println)
)

// https://atcoder.jp/contests/abs/submissions/21004768
