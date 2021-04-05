# https://atcoder.jp/contests/abs/tasks/abc087_b

class Object
  def let(f)
    f.call self
  end
end

[gets, gets, gets, gets]
  .map(&:to_i)
  .let(->((a, b, c, x)) {
    (0..[a, (x / 500).floor].min)
      .map { |i|
        (0..[b, ((x - 500 * i) / 100).floor].min)
          .filter { |j| x - 500 * i - 100 * j <= 50 * c }
          .size
      }
      .sum
  })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21519442
