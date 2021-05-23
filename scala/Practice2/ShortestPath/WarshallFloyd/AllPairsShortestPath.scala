// https://judge.u-aizu.ac.jp/onlinejudge/description.jsp?id=GRL_1_C

object Main {

  implicit class ScopeFunction[T](x: T) {
    def let[S](f: T => S): S = f(x)
  }

  def main(args: Array[String]) = (
    io.StdIn.readLine
      .split(" ")
      .map(_.toInt)
      .let(x => (x(0), x(1)))
      .let { case (v, e) =>
        (
          Array.fill(v, v)(4000000000L),
          Stream.range(0, e).map(_x =>
            io.StdIn.readLine
              .split(" ")
              .let(x => (x(0).toInt, x(1).toInt, x(2).toLong))
          )
        )
      }
      .let { case (graph, stds) => (
        (
          Stream.range(0, graph.length).foreach(i => graph(i)(i) = 0),
          stds.foreach { case (s, t, d) => graph(s)(t) = d }
        )
        .let(_x =>
          for (
            k <- 0 until graph.length;
            i <- 0 until graph.length;
            j <- 0 until graph.length
          ) (
            graph(i)(j) = graph(i)(j).min(graph(i)(k) + graph(k)(j))
          )
        )
        .let(_x =>
          if (Stream.range(0, graph.length).exists(i => graph(i)(i) < 0))
            "NEGATIVE CYCLE"
          else
            graph.map(_.map(x => if (x > 2000000000L) "INF" else x.toString).mkString(" ")).mkString("\n")
        )
      )}
      .let(println)
  )
}

// https://judge.u-aizu.ac.jp/onlinejudge/review.jsp?rid=5500307
