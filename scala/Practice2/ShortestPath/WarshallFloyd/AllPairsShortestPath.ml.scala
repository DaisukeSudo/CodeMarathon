// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_C

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  /* 20000000L * 100L * 2L */
  val INF = 4000000000L

  val readInput = () =>
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      .let(x => (x(0), x(1)))
      .let { case (v, e) =>
        (
          v,
          Stream.range(0, e).map(_x =>
            io.StdIn.readLine
              .split(" ")
              .let(x => (x(0).toInt, x(1).toInt, x(2).toLong))
          )
        )
      }

  val createGraph = (v: Int, stds: Stream[Tuple3[Int, Int, Long]]) =>
    (
      Array.fill(v, v)(INF)
    )
    .let(graph =>
      (
        Stream.range(0, v).foreach(i => graph(i)(i) = 0),
        stds.foreach { case (s, t, d) => graph(s)(t) = d }
      )
      .let(_x => graph)
    )

  val warshallFloyd = (graph: Array[Array[Long]]) =>
    (
      for (
        k <- 0 until graph.length;
        i <- 0 until graph.length;
        j <- 0 until graph.length
      ) (
        graph(i)(j) = graph(i)(j).min(graph(i)(k) + graph(k)(j))
      )
    )
    .let(_x => graph)

  val isNegative = (graph: Array[Array[Long]]) =>
    Stream.range(0, graph.length).exists(i => graph(i)(i) < 0)

  val formatToOutput = (graph: Array[Array[Long]]) =>
    if (isNegative(graph))
      "NEGATIVE CYCLE"
    else
      graph.map(_.map(x => if (x > 2000000000L) "INF" else x.toString).mkString(" ")).mkString("\n")

  def main(args: Array[String]) = (
    readInput()
      .let(createGraph.tupled)
      .let(warshallFloyd)
      .let(formatToOutput)
      .let(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5498339
