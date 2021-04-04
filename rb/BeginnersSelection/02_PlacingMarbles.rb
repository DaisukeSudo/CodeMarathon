# https://atcoder.jp/contests/abs/tasks/abc081_a

class Object
  def let(f)
    f.call self
  end
end

gets
  .chomp
  .split("")
  .map(&:to_i)
  .sum
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21508228
