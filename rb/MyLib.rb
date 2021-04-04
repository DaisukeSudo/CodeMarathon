# ----- 基本 -----

class Object
  def let(f)
    f.call self
  end
end

gets
  .let(->(x) { x })
  .let(method(:puts))
