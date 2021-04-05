# https://atcoder.jp/contests/abs/tasks/abc085_c

class Object
  def let(f)
    f.call self
  end
end

gets
  .split(" ")
  .map(&:to_i)
  .let(->((n, y)) { [n, y / 1000] })
  .let(->((n, y)) {
    (0..n)
      .find { |a|
        (y - n - 9 * a)
          .let(->(x) { x % 4 == 0 && x >= 0 && n - a - x / 4 >= 0 })
      }
      &.let(->(a) {
        ((y - n - 9 * a) / 4)
          .let(->(b) { "%d %d %d" % [a, b, n - a - b] })
      }) ||
      "-1 -1 -1"
  })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21525198
