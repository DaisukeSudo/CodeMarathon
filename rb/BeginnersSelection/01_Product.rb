# https://atcoder.jp/contests/abs/tasks/abc086_a

class Object
  def let(f)
    f.call self
  end
end

gets
  .split(" ")
  .map(&:to_i)
  .map { |x| x & 1 }
  .inject { |a, x| a & x }
  .let(->(x) { x == 1 ? 'Odd' : 'Even' })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21501400
