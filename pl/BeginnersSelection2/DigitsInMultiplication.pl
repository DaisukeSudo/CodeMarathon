% https://atcoder.jp/contests/abc057/tasks/abc057_c

read_string(Str) :-
  current_input(Input),
  read_string(Input, ' \n', '', _, Str).

read_number(Num) :-
  read_string(Str),
  number_string(Num, Str).

digits_number(Num, Len) :-
  number_string(Num, Str),
  string_length(Str, Len).

search(N, L) :-
  N1 is sqrt(N),
  N2 is ceiling(N1),
  between(1, N2, A),
  divmod(N, A, B, 0),
  digits_number(A, AL),
  digits_number(B, BL),
  L is max(AL, BL).

solve(N, Ret) :-
  findall(L, search(N, L), Ls),
  min_list(Ls, Ret).

main :-
  read_number(N),
  solve(N, Ret),
  write(Ret).

:- main.

% https://atcoder.jp/contests/abc057/submissions/17334202
