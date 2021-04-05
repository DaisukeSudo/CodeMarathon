# https://atcoder.jp/contests/abs/tasks/abc083_b

class Object
  def let(f)
    f.call self
  end
end

gets
  .split(" ")
  .map(&:to_i)
  .let(->((n, a, b)) {
    (1..n)
      .filter { |i|
        i.to_s
          .split("")
          .map(&:to_i)
          .sum
          .let(->(x) { a <= x && x <= b })
      }
      .sum
  })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21519650
