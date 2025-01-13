import { BarsIcon, XMarkIcon } from "./icons";
import { NoteList } from "./noteList";
import { PaginationComponent } from "./pagination";
import { SearchBar } from "./searchBar";
import {
  Drawer,
  DrawerClose,
  DrawerContent,
  DrawerDescription,
  DrawerHeader,
  DrawerTitle,
  DrawerTrigger,
} from "./ui/drawer";

export const MobileMenu = () => {
  return (
    <Drawer direction="left">
      <DrawerTrigger>
        <BarsIcon />
      </DrawerTrigger>
      <DrawerContent>
        <DrawerHeader className="justify-items-end">
          <DrawerClose>
            <XMarkIcon />
          </DrawerClose>
          <DrawerTitle className="hidden"></DrawerTitle>
          <DrawerDescription className="hidden"></DrawerDescription>
        </DrawerHeader>
        <SearchBar />
        <NoteList />
        <div className="flex-grow-0 mb-3">
          <PaginationComponent />
        </div>
      </DrawerContent>
    </Drawer>
  );
};
