import { forwardRef } from "react";

import MuiLink, { type LinkProps as MuiLinkProps } from "@mui/material/Link";
import { createLink } from "@tanstack/react-router";

const MUILinkAnchor = forwardRef<
  HTMLAnchorElement,
  MuiLinkProps & React.AnchorHTMLAttributes<HTMLAnchorElement>
>((props, ref) => {
  return (
    <MuiLink
      {...props}
      ref={ref}
      component="a"
    />
  );
});

MUILinkAnchor.displayName = "MUILinkAnchor";

const Link = createLink(MUILinkAnchor);

export default Link;
