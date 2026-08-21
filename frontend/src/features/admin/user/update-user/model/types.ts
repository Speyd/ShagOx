export type UpdateUserRequest = {
  id: number;
  data: UpdateUserRequestDto;
};

export type UpdateUserRequestDto = {
  surname: string;
  name: string;
  phone: string;
  email: string;
  avatar?: File | null;
  cityId: number | null;
};
