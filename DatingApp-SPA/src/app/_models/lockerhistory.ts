export interface LockerHistory {
  id: number;
  lockerId: number;
  lockerUserId: number;
  lockerCheckIn: Date;
  lockerCheckOut: Date;
  lockerUsername: string;
}
