import { useEffect, useState } from "react";

/** Scroll-spy: id of the section currently in the upper part of the viewport. */
export function useActiveSection<T extends string>(ids: readonly T[]) {
  const [active, setActive] = useState<T>(ids[0]);

  useEffect(() => {
    const elements = ids
      .map((id) => document.getElementById(id))
      .filter((el): el is HTMLElement => el !== null);

    if (elements.length === 0) return;

    const observer = new IntersectionObserver(
      (entries) => {
        const visible = entries
          .filter((entry) => entry.isIntersecting)
          .sort((a, b) => a.boundingClientRect.top - b.boundingClientRect.top);

        if (visible[0]) setActive(visible[0].target.id as T);
      },
      { rootMargin: "-30% 0px -60% 0px" },
    );

    elements.forEach((el) => observer.observe(el));
    return () => observer.disconnect();
  }, [ids]);

  return [active, setActive] as const;
}
