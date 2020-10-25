% https://atcoder.jp/contests/abc082/tasks/abc082_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, '\n', ' ', _, Str).

result_y('Yes').
result_n('No').

solve(S, T, Ret) :-
  string_chars(S, Ss),
  string_chars(T, Ts),
  sort(0, @=<, Ss, Ss2),
  sort(0, @>=, Ts, Ts2),
  compare(<, Ss2, Ts2) ->
    result_y(Ret);
    result_n(Ret).

main :-
  read_string(S),
  read_string(T),
  solve(S, T, Ret),
  writeln(Ret).

:- main.

% https://atcoder.jp/contests/abc082/submissions/17014699
