% https://atcoder.jp/contests/abc069/tasks/abc069_b

read_string(Str) :-
  current_input(Input),
  read_string(Input, '\n', '', _, Str).

head([X | _], X).

solve(S, Ret) :-
  string_chars(S, Cs),
  length(Cs, Count),
  Num is Count -2,
  number_string(Num, NumStr),
  head(Cs, S1),
  last(Cs, S2),
  concat_atom([S1, NumStr, S2], '', Ret).

main :-
  read_string(S),
  solve(S, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc069/submissions/17011593
