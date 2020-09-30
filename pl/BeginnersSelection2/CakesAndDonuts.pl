% https://atcoder.jp/contests/abc105/tasks/abc105_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

satisfy(N) :-
  between(0, N, A),
  between(0, N, B),
  N =:= 4 * A + 7 * B.

result_y('Yes').
result_n('No').

solve(N, Ret) :-
  satisfy(N) ->
    result_y(Ret);
    result_n(Ret).

main :-
  read_number(N),
  solve(N, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc105/submissions/17118893
