'use client';

import clsx from 'clsx';
// import Link from 'next/link';
// import { usePathname } from 'next/navigation';

export function NavItem({
  // href,
  children,
  isSelected,
}: {
  // href: string;
  children: React.ReactNode;
  isSelected: boolean;
}) {
  // const pathname = usePathname();

  return (
    <div
      className={clsx(
        'flex items-center gap-3 rounded-lg  px-3 py-2 text-gray-900  transition-all hover:text-gray-900  dark:text-gray-50 dark:hover:text-gray-50',
        {
          'bg-gray-100 dark:bg-gray-800': isSelected === true
        }
      )}
    >
      {children}
    </div>
  );
}