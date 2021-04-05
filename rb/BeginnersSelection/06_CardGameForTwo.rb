# https://atcoder.jp/contests/abs/tasks/abc088_b

class Object
  def let(f)
    f.call self
  end
end

[gets, gets][1]
  .split(" ")
  .map(&:to_i)
  .sort { |a, b| b <=> a }
  .reduce([0, 0, 0]) { |(a, b, i), x|
    i % 2 == 0 ?
      [a + x, b, i + 1] :
      [a, b + x, i + 1]
  }
  .let(->((a, b)) { a - b })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21520118
