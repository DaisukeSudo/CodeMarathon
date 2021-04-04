# https://atcoder.jp/contests/abs/tasks/abc081_b

class Object
  def let(f)
    f.call self
  end
end

[gets, gets][1]
  .chomp
  .split(" ")
  .map { |x| x.to_i.to_s(2) }
  .map { |x| x.length - x.rindex('1') - 1 }
  .min
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21508284
