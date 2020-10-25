% https://atcoder.jp/contests/abc095/tasks/abc095_a

read_string(Str) :-
  current_input(Input),
  read_string(Input, '\n', '', _, Str).

solve(S, Ret) :-
  string_chars(S, Cs),
  findall(_, (
    member(C, Cs),
    C == 'o'
  ), Found),
  length(Found, Count),
  Ret is 700 + 100 * Count.

main :-
  read_string(S),
  solve(S, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc095/submissions/17011450
