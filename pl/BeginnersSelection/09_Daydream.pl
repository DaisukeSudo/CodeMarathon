% https://atcoder.jp/contests/abs/tasks/arc065_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

condition([]).
condition([m, a, e, r, d        |Rest]) :- condition(Rest).
condition([r, e, m, a, e, r, d  |Rest]) :- condition(Rest).
condition([e, s, a, r, e        |Rest]) :- condition(Rest).
condition([r, e, s, a, r, e     |Rest]) :- condition(Rest).

result_Y('YES').
result_N('NO').

solve(S, Ret) :-
  string_chars(S, Chars),
  reverse(Chars, Reversed),
  condition(Reversed) ->
    result_Y(Ret);
    result_N(Ret).

main :-
  read_string(S),
  solve(S, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abs/submissions/16086996
