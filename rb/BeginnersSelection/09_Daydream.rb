# https://atcoder.jp/contests/abs/tasks/arc065_a

class Object
  def let(f)
    f.call self
  end
end

gets
  .chomp
  .let(->(s) {
    ["eraser", "erase", "dreamer", "dream"]
      .reduce([s]) { |acc, word|
        acc.flat_map { |x|
          x.split(word).filter { |x| x != "" }
        }
      }
  })
  .size
  .let(->(x) { x == 0 ? "YES" : "NO" })
  .let(method(:puts))

# https://atcoder.jp/contests/abs/submissions/21525265
