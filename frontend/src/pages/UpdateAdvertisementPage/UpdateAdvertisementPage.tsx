import UpdateAdvertisementForm from "@/features/update-advertisement";

export default function UpdateAdvertisementPage() {
  const advertisementId = Number(window.location.pathname.split("/").pop());
  return (
    <div>
      <UpdateAdvertisementForm advertisementId={advertisementId} />
    </div>
  );
}
