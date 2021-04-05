# https://atcoder.jp/contests/abs/tasks/abc085_b

class Object
  def let(f)
    f.call self
  end
end

gets
  .to_i
  .let(->(n) { (1..n) })
  .map { |_| gets }
  .uniq
  .size
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21520385
