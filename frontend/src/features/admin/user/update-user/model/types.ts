export type UpdateUserRequest = {
  id: number;
  data: UpdateUserRequestDto;
};

export type UpdateUserRequestDto = {
  firstName: string;
  lastName: string;
  userName: string;
  bio: string;
  avatar: File | null;
  cityId: number | null;
};
